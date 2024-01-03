"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl('/priceupdatehub').build();

connection.on("ReceivedInventory", function (inventory) {
	//alert('Connected to priceupdatehub on');

	BindProductsToGrid(inventory);
});

$(function () {
	connection.start().then(function () {
		connection.invoke("SendProducts").catch(function (err) {
			return console.error(err.toString());
		});
	}).catch(function (err) {
		return console.error(err.toString());
	});
});

//Inventory
connection.invoke("SendProducts").catch(function (err) {
    return console.error(err.toString());
});

function BindProductsToGrid(inventory) {
	//console.dir(inventory); - writes the object to the console log

	// Format the price above to USD using the locale, style, and currency.
	let USDollar = new Intl.NumberFormat('en-US', {
		style: 'currency',
		currency: 'USD',
	});

	$('#tblInventory tbody').empty();

	var tr;

	inventory.forEach((priceList) => {
		tr = $('<tr/>');
			tr.append(`<td>${priceList.priceListName}</td>`);
			$('#tblInventory').append(tr);

		priceList.prices.forEach((price) => {
			tr = $('<tr/>');
			tr.append(`<td>&nbsp;</td>`);
			tr.append(`<td>${price.partID}</td>`);
			tr.append(`<td>${price.part.sku}</td>`);
			tr.append(`<td>${price.part.description}</td>`);
			tr.append(`<td>${USDollar.format(price.listPrice)}</td>`);
			$('#tblInventory').append(tr);
		});
	});
}
