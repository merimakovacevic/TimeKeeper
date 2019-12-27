var SlackBot = require("slackbots");
    var channel = "general";
    var bot = new SlackBot({
        token: "xoxp-873833553349-873822937888-876160204695-ad250b25e1eac96811b9375d974a55e7",
        name: "timekeeperbot"
    });
    bot.on("start", function() {
        bot.postMessageToChannel(channel, "Hello world!");
        console.log("Hello world!");
    })
    // reply
    bot.on("message", function(data) {
        if (data.type !== "message") {
            return;
        }
        handleMessage(data.text);
    })
    function handleMessage(message) {
        switch(message) {
            case "hi":
            case "hello":
            case "dobro vece":
                sendGreeting();
                break;
            default:
                return;
        }
    }
    function sendGreeting() {
        var greeting = getGreeting();
        bot.postMessageToChannel(channel, greeting);
    }
    function getGreeting() {
        var greetings = [
            "hello!",
            "hi there!",
            "how do you do!",
            "¡hola!"
        ];
        return greetings[Math.floor(Math.random() * greetings.length)];
    }