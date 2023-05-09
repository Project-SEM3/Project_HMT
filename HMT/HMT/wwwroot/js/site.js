const requestStationery = document.getElementById("requestStationery");
const manager = document.getElementById("manager");
const requestStationeryBtn = document.getElementById("requestStationeryBtn");
const requestIdInput = document.getElementById("requestId");



const getData = (async () => {
    let requestIdValue;
    if (requestIdInput) {
        requestIdValue = requestIdInput.value;
    }

    const responseM = await fetch(`http://localhost:5096/Requests/GetManagers`);
    const responseC = await fetch('http://localhost:5096/Requests/GetCategories');
    const responseP = await fetch('http://localhost:5096/Requests/GetProducts?categoryId=1');
    const responseR = await fetch(`http://localhost:5096/Requests/GetRequestDetails?requestId=${requestIdValue}`);
    const { dataManageres } = await responseM.json();
    const { dataCategories } = await responseC.json();
    const { dataProducts } = await responseP.json();
    const { dataRequest } = await responseR.clone().json();
    const { dataRequestDetails } = await responseR.clone().json();

    if (manager) {
        let innerManager = `
         <div class="grid grid-cols-3">
            <div class="mb-6">
              <label for="manager" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select review manager</label>
              <select name="manager" id='manager'
                class="outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
                ${dataManageres ? dataManageres.map(item => `
                    <option value="${item.id}" ${dataRequest.userTo ? "selected" : ""}> 
                      <div class="flex items-center gap-3">
                        <img class="w-10 h-10 rounded-md object-cover" src="~/img/${item.image}"
                          alt="mana">
                        <span>${item.fullName}</span>
                      </div>
                    </option>`).join(" ") :
                ` `
            }
              </select>
            </div>
          </div>   
        `;
        manager.innerHTML = innerManager;
    }


    if (requestStationery) {
        let innerStationery = ``;
        if (requestIdValue) {
            innerRequestEidt();
        } else {
            innerRequset();
        }

        function innerRequestEidt() {
            dataRequestDetails.map(item => {
                innerStationery = `
                    <input type="text" name="requestDetails" value=${item.id} class="hidden"/>
                    <div class="col-span-2">
	                    <label for="category" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select
		                    stationery</label>
                        <input name="category" id="category" type="number" value=${item.categoryId} class="hidden"/>
	                    <input  type="text" value="${item.categoryName}" disabled
		                    class="category disabled outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
	                    </input>
                    </div >
                    <div class="col-span-3">
	                    <label for="product" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select
		                    stationery type</label>
	                    <select name="product" id="product" data-categoryid="${item.id}"
		                    class="product outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
		                   ${renderProductsEdit(item.categoryId, item.id, item.productId)}
	                    </select>
                    </div>
                    <div>
	                    <label for="quantity" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Quantity</label>
	                    <input name="quantity" type="number" id="quantity"
		                    class="bg-gray-50 border border-gray-300 text-gray-900 outline-none text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
		                    value=${item.qty}  required min="1">
                    </div>
                    <div class="col-span-3">
	                    <label for="note" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Note</label>
	                    <input name="note" type="text" id="note"
		                    class="bg-gray-50 border border-gray-300 text-gray-900 outline-none text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
		                    ${item.note == "" ? `placeholder="Empty"` : `value=${item.note}`}>
                    </div>
                    <div class="flex flex-col items-center ">
	                    <h5 class="block mb-2 text-sm font-medium text-gray-900 dark:text-white text-center">Action</h5>
	                    <button type="button"
		                    class="text-white rounded-md bg-rose-500 py-2 px-4 hover:bg-rose-700 deleteRequest">Delete</button>
                    </div> `
                renderRequest()
                return requestIdValue;

            })
        }

        function innerRequset() {
            innerStationery = `
                <input type="text" name="requestDetails" value="0" class="hidden"/>
                <div class="col-span-2">
	                <label for="category" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select
		                stationery</label>
	                <select name="category" id="category"
		                class="category outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
		               ${dataCategories ? dataCategories.map(item => `<option value="${item.id}">${item.nameCategory}</option>`).join(" ") : ` `}
	                </select>
                </div>
                <div class="col-span-3">
	                <label for="product" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select
		                stationery type</label>
	                <select name="product" id="product"
		                class="product outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
		               ${dataProducts ? dataProducts.map(item => `<option value="${item.id}">${item.nameProduct}</option>`).join(" ") : `<option value="null">Please choose other stationery</option>`}
	                </select>
                </div>

                <div>
	                <label for="quantity" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Quantity</label>
	                <input name="quantity" type="number" id="quantity"
		                class="bg-gray-50 border border-gray-300 text-gray-900 outline-none text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
		                value="1" required min="1">
                </div>
                <div class="col-span-3">
	                <label for="note" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Note</label>
	                <input name="note" type="text" id="note"
		                class="bg-gray-50 border border-gray-300 text-gray-900 outline-none text-sm rounded-lg focus:border-primary block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
		                placeholder="Vd: Thien Long Pen">
                </div>
                <div class="flex flex-col items-center ">
	                <h5 class="block mb-2 text-sm font-medium text-gray-900 dark:text-white text-center">Action</h5>
	                <button type="button"
		                class="text-white rounded-md bg-rose-500 py-2 px-4 hover:bg-rose-700 deleteRequest">Delete</button>
                </div>
                `;
            renderRequest();
        }

        function deteleRequestRow() {
            let btnDeleteStationery = document.querySelectorAll(".deleteRequest");

            btnDeleteStationery.forEach((item) => {
                item.addEventListener("click", () => {
                    requestStationery.removeChild(item.parentNode.parentNode);
                });
            });
        }

        function renderRequest() {
            let newStationery = document.createElement("div");
            newStationery.className = "grid grid-cols-10 gap-3 mb-6";
            newStationery.innerHTML = innerStationery;
            requestStationery.appendChild(newStationery);
        }

        requestStationeryBtn.addEventListener("click", () => {
            innerRequset();
            deteleRequestRow();
            selectCategory();
        });

        deteleRequestRow();
        selectCategory();
    }
})
getData()
selectCategory();


function selectCategory() {
    let categoryStationery = document.querySelectorAll(".category");

    categoryStationery.forEach((category) => {
        category.addEventListener("change", async () => {
            let categoryID = category.value;
            const responseP = await fetch(`http://localhost:5096/Requests/GetProducts?categoryId=${categoryID}`);

            // fetch product data based on categoryID
            const { dataProducts } = await responseP.json();
            // update select of product with options created from fetched data
            const productSelect = category.parentNode.parentNode.querySelector(".product");
            productSelect.innerHTML = dataProducts ?
                dataProducts.map(item => `<option value="${item.id}">${item.nameProduct}</option>`).join(" ")
                :
                `<option value="null">Please choose other stationery</option>`
        });
    });
}

async function renderProductsEdit(categoryID, requestDetailID, productID) {
    const responseP = await fetch(`http://localhost:5096/Requests/GetProducts?categoryId=${categoryID}`);
    // fetch product data based on categoryID
    const { dataProducts } = await responseP.json();

    // Lấy thẻ select từ DOM dựa trên categoryId
    var productSelect = document.querySelector('select[data-categoryid="' + requestDetailID + '"]');

    console.log(productID)

    // update select of product with options created from fetched data
    productSelect.innerHTML = dataProducts ?
        dataProducts.map(item => `<option value="${item.id}" ${item.id == productID ? "selected" : ""}>${item.nameProduct}</option>`).join(" ")
        :
        `<option value="null">Please choose other stationery</option>`
}