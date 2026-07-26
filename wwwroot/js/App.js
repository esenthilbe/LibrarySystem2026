const apiBase = "https://localhost:7083/api"; // adjust port if different

// Handle Add Book form
document.getElementById("bookForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const title = document.getElementById("title").value;
    const author = document.getElementById("author").value;

    const book = { Title: title, Author: author };
    
    const response = await fetch(`${apiBase}/books`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(book)
    });

    if (response.ok) {
        alert("Book added!");
        getBooks();
    } else {
        const errorText = await response.text();
        //console.error("Server error:", errorText);
        alert("Error adding book");
    }
});

// Fetch and display books
async function getBooks() {
    const response = await fetch(`${apiBase}/books`);
    const books = await response.json();

    const list = document.getElementById("bookList");
    list.innerHTML = "";

    books.forEach(book => {
        const li = document.createElement("li");
        li.textContent = `${book.title} by ${book.author} - ${book.isAvailable ? "Available" : "Borrowed"}`;

        if (book.isAvailable) {
            const borrowBtn = document.createElement("button");
            borrowBtn.textContent = "Borrow";
            borrowBtn.onclick = () => borrowBook(book.id, 2); // hardcoded memberId=2 for demo
            li.appendChild(borrowBtn);
        } else {
            const returnBtn = document.createElement("button");
            returnBtn.textContent = "Return";
            returnBtn.onclick = () => returnBook(book.id);
            li.appendChild(returnBtn);
        }

        list.appendChild(li);
    });
}
async function borrowBook(bookId, memberId) {
    const response = await fetch(`${apiBase}/books/borrow/${bookId}?memberId=${memberId}`, {
        method: "PUT"
    });
    const result = await response.text();
    alert(result);
    getBooks();
}

async function returnBook(bookId) {
    const response = await fetch(`${apiBase}/books/return/${bookId}`, {
        method: "PUT"
    });
    const result = await response.text();
    alert(result);
    getBooks();
}
