var app = require('express')();
var http = require('http').Server(app);

app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');

app.get('/', function(req, res){
	res.render('index');
});

http.listen(3000, function(){
  console.log('listening on *:3000');
});