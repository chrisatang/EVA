var restify = require('restify')
var builder = require('botbuilder');
var commands = require('./index.js');

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
// TODO: store functions in exterior .JS file (to be created)

// Call a series of functions depending on user input
var userInput = session.message.text;
// Receive messages from the user and respond by echoing the user's messages
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("You said: %s", userInput);
});