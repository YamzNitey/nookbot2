using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;

namespace nookbot2.Modules
{
    public class General : ModuleBase<SocketCommandContext>
    {
        [Command("hello")]
        public async Task SayHello()
        {
            await ReplyAsync("Hello, World!");
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

        [Command("membercount")]
        public async Task GetMemberCount()
        {
            await ReplyAsync($"{Context.Guild.Name} has {Context.Guild.MemberCount} members.");
        }

        [Command("headsortails")]
        [Alias("hot", "heads", "tails")]
        public async Task HeadsOrTails()
        {
            Random rnd = new Random();
            int result = rnd.Next(0, 2);

            if (result == 1)
                await ReplyAsync("Heads.");
            else
                await ReplyAsync("Tails.");
        }
    }
}