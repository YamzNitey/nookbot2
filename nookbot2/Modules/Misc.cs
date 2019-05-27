﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace nookbot2.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("hello")]
        public async Task SayHello()
        {
            await ReplyAsync("Hello, World!");
        }

        [Command("say")]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task Say([Remainder]string message)
        {
            await Say(Context.Channel, message);
        }

        [Command("say")]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task Say(ISocketMessageChannel channel, [Remainder]string message)
        {
            await channel.SendMessageAsync(message);
        }

        [Command("avatar")]
        public async Task GetAvatar(SocketGuildUser user)
        {
            string avatarUrl = user.GetAvatarUrl(size: 1024);
            await ReplyAsync($"`{user}`'s avatar is {avatarUrl}");
        }

        [Command("source")]
        [Alias("sourcecode", "sauce")]
        public async Task GetSourceLink()
        {
            await ReplyAsync("https://github.com/Yamz9983/nookbot2");
        }
    }
}