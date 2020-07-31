const uri = 'api/Books';
let todos = [];

function getItems() {
    debugger
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addBookIDTextbox = document.getElementById('add-bookID');
    const addBookNameTextbox = document.getElementById('add-bookName');
    const addPriceTextbox = document.getElementById('add-price');
    const addCategoryTextbox = document.getElementById('add-category');
    const addAuthorTextbox = document.getElementById('add-author');

    const item = {
        bookID: addBookIDTextbox.value.trim(),
        bookName: addBookNameTextbox.value.trim(),
        price: parseFloat(addPriceTextbox.value.trim()),
        category: addCategoryTextbox.value.trim(),
        author: addAuthorTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addBookNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.bookID === id);
    if (item != undefined) {        
        document.getElementById('edit-bookID').value = item.bookID;
        document.getElementById('edit-bookName').value = item.bookName;
        document.getElementById('edit-price').value = item.price;
        document.getElementById('edit-category').value = item.category;
        document.getElementById('edit-author').value = item.author;
        document.getElementById('editForm').style.display = 'block';
    }
    
}

function updateItem() {
    const editBookIDTextbox = document.getElementById('edit-bookID');
    const editBookNameTextbox = document.getElementById('edit-bookName');
    const editPriceTextbox = document.getElementById('edit-price');
    const editCategoryTextbox = document.getElementById('edit-category');
    const editAuthorTextbox = document.getElementById('edit-author');

    const item = {
        bookID: editBookIDTextbox.value.trim(),
        bookName: editBookNameTextbox.value.trim(),
        price: parseFloat(editPriceTextbox.value.trim()),
        category: editCategoryTextbox.value.trim(),
        author: editAuthorTextbox.value.trim()
    };

    fetch(`${uri}/${item.bookID}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'to-do' : 'to-dos';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
    

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm("${item.bookID}")`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem("${item.bookID}")`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.bookID);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.bookName);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.price);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        let textNode4 = document.createTextNode(item.category);
        td4.appendChild(textNode4);

        let td5 = tr.insertCell(4);
        let textNode5 = document.createTextNode(item.author);
        td5.appendChild(textNode5);

        let td6 = tr.insertCell(5);
        td6.appendChild(editButton);

        let td7 = tr.insertCell(6);
        td7.appendChild(deleteButton);
    });

    todos = data;
}