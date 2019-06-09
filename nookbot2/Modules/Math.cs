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
        [Command("add", true)]
        public async Task Add(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` + `{num2}` = `{num1 + num2}`");
        }

        [Command("subtract", true)]
        [Alias("sub")]
        public async Task Subtract(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` - `{num2}` = `{num1 - num2}`");
        }

        [Command("multiply", true)]
        [Alias("mul")]
        public async Task Multiply(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` * `{num2}` = `{num1 * num2}`");
        }

        [Command("divide", true)]
        [Alias("div")]
        public async Task Divide(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` / `{num2}` = `{num1 / num2}`");
        }

        [Command("modulo", true)]
        [Alias("mod")]
        public async Task Modulo(double num1, double num2)
        {
            await ReplyAsync($"Sum: `{num1}` % `{num2}` = `{num1 % num2}`");
        }

        [Command("and", true)]
        public async Task And(long num1, long num2)
        {
            await ReplyAsync($"Result: `{num1}` & `{num2}` = `{num1 & num2}`");
        }

        [Command("or", true)]
        public async Task Or(long num1, long num2)
        {
            await ReplyAsync($"Result: `{num1}` | `{num2}` = `{num1 | num2}`");
        }

        [Command("xor", true)]
        public async Task Xor(long num1, long num2)
        {
            await ReplyAsync($"Result: `{num1}` ^ `{num2}` = `{num1 ^ num2}`");
        }

        [Command("not", true)]
        public async Task Not(long num)
        {
            await ReplyAsync($"Result: ~`{num}` = `{~num}`");
        }

        [Command("tc", true)]
        public async Task TwosComplement(long num)
        {
            await ReplyAsync($"Result: ~`{num}` + 1 = `{~num + 1}`");
        }

        [Command("leftshift")]
        [Alias("ls")]
        public async Task LeftShift(long num, int bitAmount)
        {
            await ReplyAsync($"Result: `{num}` << `{bitAmount}` = `{num << bitAmount}`");
        }

        [Command("rightshift")]
        [Alias("rs")]
        public async Task RightShift(long num, int bitAmount)
        {
            await ReplyAsync($"Result: `{num}` >> `{bitAmount}` = `{num >> bitAmount}`");
        }
    }
}
