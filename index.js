var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);

app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');

app.get('/', function(req, res){
	//res.render('index');
	res.sendFile(__dirname + '/index.html');
});

io.on('connection', function(socket){
	console.log("a user connected");

  socket.on('chat message', function(msg){
  	io.emit('chat message', msg);

  	if(msg.toLowerCase().indexOf("where is") != -1){
  		io.emit('where', msg);
  	}
    console.log('message: ' + msg);
  });
});

http.listen(3000, function(){
  console.log('listening on *:3000');
});