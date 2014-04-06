// 必要なモジュールを読み込みます。
var http = require("http");
var socketIO = require("socket.io");
var fs = require("fs");

// node.jsでWebServerを作ります。
// アクセスされたら、クライアントに表示するsyncCanvas.htmlを返します。
var server = http.createServer(function (req, res) {
    res.writeHead(200, { "Content-Type": "text/html" });
    var output = fs.readFileSync("./syncCanvas.html", "utf-8");
    res.end(output);
});

server.listen(process.env.PORT, process.env.IP);

// socket.IOを用いたリアルタイムWebを実装します。
var io = socketIO.listen(server);

// 接続されたら、connected!とコンソールにメッセージを表示します。
io.sockets.on("connection", function (socket) {
    console.log("connected");

    // 描画情報がクライアントから渡されたら、接続中の他ユーザーへ
    // broadcastで描画情報を送ります。
    socket.on("draw", function (data) {
        console.log(data);
        socket.broadcast.emit("draw", data);
    });

    // 色変更情報がクライアントからきたら、
    // 他ユーザーへ変更後の色を通知します。
    socket.on("color", function (color) {
        console.log(color);
        socket.broadcast.emit("color", color);
    });

    // 線の太さの変更情報がクライアントからきたら、
    // 他ユーザーへ変更後の線の太さを通知します。
    socket.on("lineWidth", function (width) {
        console.log(width);
        socket.broadcast.emit("lineWidth", width);
    });

    // 一斉送信通知が来たら接続中の他ユーザーへ
    // broadcastで描画情報を送ります。
    socket.on("callAll", function (data) {
        console.log(data);
        socket.broadcast.emit("callAll", data);
    });

});