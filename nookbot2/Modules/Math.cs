using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace nookbot2.Modules
{
    public class Math : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task Add(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` + `{num2}` = `{num1 + num2}`");
        }

        [Command("subtract")]
        [Alias("sub")]
        public async Task Subtract(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` - `{num2}` = `{num1 - num2}`");
        }

        [Command("multiply")]
        [Alias("mul")]
        public async Task Multiply(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` * `{num2}` = `{num1 * num2}`");
        }

        [Command("divide")]
        [Alias("div")]
        public async Task Divide(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` / `{num2}` = `{num1 / num2}`");
        }

        [Command("modulo")]
        [Alias("mod")]
        public async Task Modulo(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` % `{num2}` = `{num1 % num2}`");
        }
    }
}
