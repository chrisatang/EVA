var restify = require('restify')
var builder = require('botbuilder');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log('%s listening to %s', server.name, server.url);
});

// Create chat connector to communicate with Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

// Listen to messages from client
server.post('/api/messages', connector.listen());

// Receive messages from the user and respond by echoing the user's messages
var bot = new builder.UniversalBot(connector);

bot.dialog('/',
    function(session){
        session.beginDialog()
    }
);

bot.dialog("RequestOrder",[
    function(session){
        builder.Prompts.texts(session,"Welcome to EVA how can I help you");
    },
    function(session,results){
        session.endDialog("You said ${results.response}");
    }
])
