// Sidebar Toggle
const hamburger = document.querySelector(".toggle-btn");
const toggler = document.querySelector("#icon");
if (hamburger && toggler) {
    hamburger.addEventListener("click", function () {
        document.querySelector("#sidebar").classList.toggle("expand");
        toggler.classList.toggle("bxs-chevrons-right");
        toggler.classList.toggle("bxs-chevrons-left");
    });
}

// Toggle Sidebar Dropdown

const items = document.querySelectorAll(".has-dropdown");

function toggle() {
    const itemToggle = this.classList.contains("active");

    items.forEach((item) => item.classList.remove("active"));

    if (!itemToggle) {
        this.classList.add("active");
    }
}

items.forEach((item) => item.addEventListener("click", toggle));
// User link dropdown

const userProfile = document.querySelector(".user-link");

if (userProfile) {
    userProfile.addEventListener("click", function () {
        document.querySelector(".user-link-dropdown").classList.toggle("show");
    });
}

// Notification bell dropdown
const notificationBell = document.querySelector('.notification-bell');
const notificationDropdown = document.querySelector('.notification-dropdown');
if (notificationBell && notificationDropdown) {
    notificationBell.addEventListener('click', function (e) {
        e.preventDefault();
        notificationDropdown.classList.toggle('show');
        // Hide user dropdown if open
        const userDropdown = document.querySelector('.user-link-dropdown');
        if (userDropdown) userDropdown.classList.remove('show');
    });
    // Close dropdown when clicking outside
    document.addEventListener('click', function (e) {
        if (!notificationBell.contains(e.target) && !notificationDropdown.contains(e.target)) {
            notificationDropdown.classList.remove('show');
        }
    });
}

const ctx = document.getElementById('myChart');
if (ctx && window.Chart) {
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: [
                'Red',
                'Blue',
                'Yellow'
            ],
            datasets: [{
                label: 'My First Dataset',
                data: [300, 50, 100],
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    'rgb(255, 205, 86)'
                ],
                hoverOffset: 4
            }]
        },

    });
}

//DatatableJs
if (window.DataTable) {
    let table = new DataTable("#main-table", {
        pageLength: 5,
    });
}

