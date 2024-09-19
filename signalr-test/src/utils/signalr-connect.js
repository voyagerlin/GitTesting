import * as signalR from "@microsoft/signalr";

export const canClose = false;
const signalrConn = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7004/notifyHub")
  .configureLogging((logging) => {
    logging.SetMinimumLevel(LogLevel.Information);
    logging.AddConsole();
  })
  .build();

signalrConn.on("SetConnId", function (user, msg) {
  console.log("vue user", user);
  console.log("vue msg", msg);
});
// conn.on("CloseModal", (isClose)=>{
//   console.log('CloseModal', isClose)
//   canClose = isClose
// })

async function start() {
  try {
    await signalrConn.start();

    console.log("Vue SignalR Connected.");
  } catch (err) {
    console.log(err);
    setTimeout(start, 5000);
  }
}

signalrConn.onclose(async (err) => {
  console.log("vue conn error:", err);
  await start();
});

// Start the connection.
start();
export default  signalrConn
