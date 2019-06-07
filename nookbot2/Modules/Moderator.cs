using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;

namespace nookbot2.Modules
{
    public class Moderator : ModuleBase<SocketCommandContext>
    {
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

        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(SocketGuildUser user, [Remainder]string reason = "No reason provided.")
        {
            // If the user is executing the command on themselves...
            if (Context.User == user)
            {
                await ReplyAsync("You cannot perform moderator actions on yourself. 😡");
            }
            else
            {
                try
                {
                    await user.KickAsync(reason: $"Kicked by: {Context.User} | Reason: {reason}");
                    await ReplyAsync($"`{user}` has been kicked.");
                }
                catch (Exception e)
                {
                    await ReplyAsync($"An error occured while attempting to kick `{user}`.");
                    Console.WriteLine(e);
                }
            }
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(SocketGuildUser user, [Remainder]string reason = "No reason provided.")
        {
            if (Context.User == user)
            {
                await ReplyAsync("You cannot perform moderator actions on yourself. 😡");
            }
            else
            {
                try
                {
                    await user.BanAsync(reason: $"Banned by: {Context.User} | Reason: {reason}");
                    await ReplyAsync($"`{user}` has been banned. 🔨");
                }
                catch (Exception e)
                {
                    await ReplyAsync($"An error occured while attempting to ban `{user}`.");
                    Console.WriteLine(e);
                }
            }
        }

        [Command("unban", true)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task UnbanUser(SocketGuildUser user)
        {
            try
            {
                await user.Guild.RemoveBanAsync(user);
                await ReplyAsync($"`{user}` has been unbanned.");
            }
            catch (Exception e)
            {
                await ReplyAsync($"An error occured while attempting to unban `{user}`.");
                Console.WriteLine(e);
            }
        }

        [Command("mute", true)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task MuteUser(SocketGuildUser user)
        {
            if (Context.User == user)
            {
                await ReplyAsync("You cannot perform moderator actions on yourself. 😡");
            }
            else
            {
                try
                {
                    IRole mutedRole = Context.Guild.Roles.FirstOrDefault(role => role.Name == "Muted");
                    await user.AddRoleAsync(mutedRole);
                    await ReplyAsync($"`{user}` has been muted.");
                }
                catch (Exception e)
                {
                    await ReplyAsync($"An error occured while attempting to mute `{user}`.");
                    Console.WriteLine(e);
                }
            }
        }

        [Command("unmute", true)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task UnmuteUser(SocketGuildUser user)
        {
            try
            {
                IRole mutedRole = Context.Guild.Roles.FirstOrDefault(role => role.Name == "Muted");
                if (user.Roles.Contains(mutedRole))
                {
                    await user.RemoveRoleAsync(mutedRole);
                    await ReplyAsync($"`{user}` has been unmuted.");
                }
                else
                {
                    await ReplyAsync($"`{user}` isn't muted.");
                }
            }
            catch (Exception e)
            {
                await ReplyAsync($"An error occured while attempting to unmute `{user}`.");
                Console.WriteLine(e);
            }
        }

        [Command("lock", true)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task LockChannel()
        {
            try
            {
                SocketGuildChannel channel = Context.Channel as SocketGuildChannel;
                await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Deny));
                await ReplyAsync("Channel locked. 🔒");
            }
            catch (Exception e)
            {
                await ReplyAsync("An error occured while attempting to lock the channel.");
                Console.WriteLine(e);
            }
        }

        [Command("unlock", true)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task UnlockChannel()
        {
            try
            {
                SocketGuildChannel channel = Context.Channel as SocketGuildChannel;
                await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Inherit));
                await ReplyAsync("Channel unlocked. 🔓");
            }

            catch (Exception e)
            {
                await ReplyAsync("An error occured while attempting to unlock the channel.");
                Console.WriteLine(e);
            }
        }

        [Command("nickname")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        [RequireBotPermission(GuildPermission.ManageNicknames)]
        public async Task SetUserNickname(SocketGuildUser user, [Remainder]string nickname)
        {
            try
            {
                await user.ModifyAsync(x => x.Nickname = nickname);
                await ReplyAsync($"`{user}`'s nickname is now `{nickname}`.");
            }
            catch(Exception e)
            {
                await ReplyAsync("An error occured while attempting to change the user's nickname.");
                Console.WriteLine(e);
            }
        }

        [Command("playing")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task SetGameStatus([Remainder]string status)
        {
            await Context.Client.SetGameAsync(status);
            await ReplyAsync($"Game status changed to `{status}`. 👍");
        }

        [Command("addrole")]
        [Alias("ar", "role")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task AddRole(SocketGuildUser user, [Remainder]string roleName)
        {
            try
            {
                IRole role = Context.Guild.Roles.FirstOrDefault(x => x.Name == roleName);

                if(user.Roles.Contains(role))
                    await ReplyAsync($"`{user}` already has the `{role.Name}` role.");
                else
                    await user.AddRoleAsync(role);
            }
            catch(Exception e)
            {
                await ReplyAsync("An exception occured while attempting to assign the role to the user. Perhaps the role doesn't exist?");
                Console.WriteLine(e);
            }
        }

        [Command("removerole")]
        [Alias("rr")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task RemoveRole(SocketGuildUser user, [Remainder]string roleName)
        {
            try
            {
                IRole role = Context.Guild.Roles.FirstOrDefault(x => x.Name == roleName);

                if (user.Roles.Contains(role))
                    await ReplyAsync($"`{user}` already has the `{role.Name}` role.");
                else
                    await user.AddRoleAsync(role);
            }
            catch(Exception e)
            {
                await ReplyAsync("An error occured while attempting to remove the role from the user. Perhaps the role doesn't exist?");
                Console.WriteLine(e);
            }
        }
    }
}