using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;

namespace nookbot2
{ 
    public class Log : ModuleBase<SocketCommandContext>
    {
        public static async Task SendReactionLog(ReactionModLog log)
            => await new Log().SendReactionLog_(log);

        public async Task SendReactionLog_(ReactionModLog log)
        {
            EmbedBuilder embed = new EmbedBuilder()
            {
                Title = log.Title,
                Color = Color.Blue,
            };

            string emoteInEmbed;
            if (log.Emote is Emote emote)
                emoteInEmbed = $"{emote} ({emote.Url})";
            else
                emoteInEmbed = log.Emote.Name;

            embed.AddField("Reaction added/removed by", log.UserReaction);
            embed.AddField("Channel", $"<#{log.ChannelID}>");
            embed.AddField("Emote", emoteInEmbed);
            embed.AddField("Date and time and message ID", log.DTAndMID);

            ISocketMessageChannel modlogChannel = Context.Client.GetChannel(Config.Bot.LogChannel) as ISocketMessageChannel;
            await modlogChannel.SendMessageAsync("", false, embed.Build());
        }
    }

    public class ReactionModLog
    {
        public string Title;
        public string UserReaction;
        public ulong ChannelID;
        public IEmote Emote;
        public string DTAndMID;
    }
}