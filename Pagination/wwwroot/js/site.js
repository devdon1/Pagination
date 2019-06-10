
function NumberOfProducts(numberOfProducts) {

    // Get the selected value
    var numberOfProductsValue = numberOfProducts.value;

    // Prepare URL for fetch call
    var url = "/Product/NumberOfProducts?" + $.param({ value: numberOfProductsValue });

    // Make an Ajax call
    //$.ajax({

    //    method: "POST",
    //    url: url,
    //    success: function (response) {
    //        window.location.replace(response);
    //        console.log(response);
    //    }
    //});

    // Execute the fetch call
    fetch(url,
        {
            method: "POST"
        }

    ).then(response => { return response.json(); }).then(response => window.location.replace(response));
}