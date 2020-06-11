# Chatty

## Prerequisites

1. Microsoft .NET Core SDK 3.1.101 (x64) -- newer might work
1. MySQL Server 8.0
1. Visual Studio Code 1.45.1
1. Opera version 69.0.3686.21 >= OR Google Chrome >= 83.0.4103.97

## DB Configuration

1. Find appsettings.json
1. Create empty schema with an account in MySQL
1. Set "DefaultConnection" parameters
1. In the terminal run command dotnet ef database update OR use file chatty.sql
1. You should now be able to launch the project.

## Testing

1. Launch the project.
1. Root account is created automatically - but there is no need to use it so we will create our own.
1. Launch browser normally and in private mode (so we will be able to simulate two users talking at the same time)
1. On both windows go to localhost:5001 (the website should automatically open in the normal browser)
1. On both windows click register and create two distinct accounts.
1. You should now be logged in two different windows on two different Accounts.
1. Go to Chat in both Windows. You should see root user and the other user (but not you).
1. In both windows click GENERATE NEW KEY PAIR (otherwise encryption won't work)
1. Select second in Recent list.
1. Type in message.
1. Click blue button (not enter!).
1. Open the second user window and select first user in the Chat list.
1. You should now see message, it's completely unreadable for third party (both during transport and when stored in DB).

## Conclusions

That concludes the demo - you can login, create account and send message safely.
There are a lot of user-experience issues, but as this is prototype focused on encryption so it doesn't matter.

Private key is stored in cookies - if cookie gets lost it needs to be generated anew.
If user sends the message, second side won't automatically receive it - as second user you need to select contact again or refresh the page.
