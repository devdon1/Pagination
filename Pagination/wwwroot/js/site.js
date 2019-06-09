
function NumberOfProducts(numberOfProducts) {

    // Get the selected value
    var numberOfProductsValue = numberOfProducts.value;

    // Prepare URL for fetch call
    var url = "/Product/NumberOfProducts?" + $.param({ value: numberOfProductsValue});

    // Execute the fetch call
    fetch(url, {method: "POST"}).then(response => console.log(response));
}