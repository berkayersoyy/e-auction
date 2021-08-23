import { signalR } from "../../../lib/microsoft-signalr/signalr";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:8001/auctionhub").build();

//Disable send button until connection is established

document.getElementById("sendButton").disabled = true;

