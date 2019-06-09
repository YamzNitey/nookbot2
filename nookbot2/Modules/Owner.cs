using System;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using System.Net;

namespace nookbot2.Modules
{
    public class Owner : ModuleBase<SocketCommandContext>
    {
        [Command("download_emotes", true)]
        [RequireOwner]
        [Alias("de")]
        public async Task DownloadEmotes()
        {
            int downloadedEmotes = 0;

            foreach (var emote in Context.Guild.Emotes)
            {
                using (WebClient wc = new WebClient())
                {
                    Uri emoteUrl = new Uri(emote.Url);

                    // Index 52 contains the file extension of the emote. (*.png, *.gif)
                    string emoteExt = emote.Url.Substring(52);

                    Directory.CreateDirectory(Context.Guild.Id.ToString());
                    wc.DownloadFileAsync(emoteUrl, $"{Context.Guild.Id}/{emote.Name}{emoteExt}");
                    downloadedEmotes++;
                }
            }

            await ReplyAsync($"Done, downloaded {downloadedEmotes} emote(s).");
        }

        [Command("number_of_emotes", true)]
        [RequireOwner]
        [Alias("noe")]
        public async Task NumberOfEmotes()
        {
            await ReplyAsync(Context.Guild.Emotes.Count.ToString());
        }
    }
}