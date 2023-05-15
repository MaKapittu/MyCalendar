# MyCalendar
Group calendar project

Made with c#, vue.ts, typescript, docker

Test are made in postman

To open a project on windows 10 and vs code:
1. In Visual Studio Code open folder with the poject

2. Open Docker to make engine running 

3. Open terminal "Terminal - New terminal"

4. Go to the "back" folder using cd and write "Docker compose up"
    Example:
        PS C:\Users\Mary\source\repos\MyCalendar> cd back
        PS C:\Users\Mary\source\repos\MyCalendar\back> docker compose up

5. When the new container is added to docker press F5 in Visual studio Code to start the backend of the project
    5.1. If you have error with HTTPS endpoint write to terminal "dotnet dev-certs https"
    5.2. This will fix the issue

6. Open new terminal "Terminal - New terminal"

7. Using terminal go to front folder using cd and write "pnpm install" to install needed packages
    Example:
        C:\Users\Mary\source\repos\MyCalendar> cd front
        C:\Users\Mary\source\repos\MyCalendar\front> pnpm install

8. In same terminal write "pnpm run dev" to run frontend of the project

9. Link for the site will apear in terminal. To open it press ctrl + left click
