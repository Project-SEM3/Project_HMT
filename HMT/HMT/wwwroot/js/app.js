const avatarDropdown = document.getElementById("avatarDropdown");
const avatarDropdownMenu = document.getElementById("avatarDropdownMenu");
const notiDropdown = document.getElementById("notiDropdown");
const notiDropdownMenu = document.getElementById("notiDropdownMenu");
const darkModeBtn = document.getElementById("darkModeBtn");
const avatarEdit = document.getElementById("avatarEdit");
const avatarEditImg = document.getElementById("avatarEditImg");
const hiddenPass = document.querySelectorAll(".hiddenPass");
const loading = document.getElementById("loading");
const passCheck = document.getElementById("passCheck");
const rePassCheck = document.getElementById("rePassCheck");
const alertPass = document.getElementById("alertPass");
const btnSubmit = document.getElementById("btnSubmit");
const btnDeleteAccount = document.querySelectorAll(".btnDelete");
const btnCloseModalAcc = document.querySelectorAll(".btnCloseModalAcc");
const totalTitle = document.querySelectorAll(".totalTitle");
const fillter = document.getElementById("fillter");
const fillterDropdown = document.getElementById("fillterDropdown");
const fillterDropdownChild = document.querySelectorAll("#fillterDropdown li");

if (avatarDropdownMenu && avatarDropdown) {
    avatarDropdown.addEventListener("click", () => {
        avatarDropdownMenu.classList.toggle("hidden");
    });
}
if (avatarDropdownMenu && avatarDropdown) {
    notiDropdown.addEventListener("click", () => {
        notiDropdownMenu.classList.toggle("hidden");
    });
}

document.addEventListener("click", function (event) {
    if (!avatarDropdown.contains(event.target)) {
        avatarDropdownMenu.classList.add("hidden");
    }
    if (!notiDropdown.contains(event.target)) {
        notiDropdownMenu.classList.add("hidden");
    }
});

if (darkModeBtn) {
    darkModeBtn.addEventListener("click", () => {
        darkModeBtn.classList.toggle("light");
        document.body.classList.toggle("dark");
        if (darkModeBtn.classList.contains("light")) {
            darkModeBtn.innerHTML = '<i class="fa-regular fa-moon"></i>';
        } else {
            darkModeBtn.innerHTML = '<i class="fa-regular fa-sun-bright"></i>';
        }
    });
}

if (avatarEdit) {
    avatarEdit.addEventListener("change", (e) => {
        avatarEditImg.src = URL.createObjectURL(e.target.files[0]);
    });
}

if (hiddenPass) {
    hiddenPass.forEach((item) => {
        item.addEventListener("click", () => {
            item.classList.toggle("viewPass");
            if (item.classList.contains("viewPass")) {
                item.innerHTML = '<i class="fa-solid fa-eye"></i>';
                item.previousElementSibling.type = "password";
            } else {
                item.innerHTML = '<i class="fa-solid fa-eye-slash"></i>';
                item.previousElementSibling.type = "text";
            }
        });
    });
}

window.addEventListener("load", () => {
    loading.classList.add("hidden");
});

if (passCheck) {
    function checkPassMatch() {
        if (passCheck.value !== rePassCheck.value) {
            alertPass.classList.remove("hidden");
            btnSubmit.classList.add("opacity-30", "cursor-not-allowed");
            btnSubmit.type = "button";
        } else {
            alertPass.classList.add("hidden");
            btnSubmit.classList.remove("opacity-30", "cursor-not-allowed");
            btnSubmit.type = "submit";
        }
    }
    rePassCheck.addEventListener("blur", () => {
        checkPassMatch();
    });
    passCheck.addEventListener("blur", () => {
        checkPassMatch();
    });
}

if (btnDeleteAccount) {
    btnDeleteAccount.forEach((btn) => {
        btn.addEventListener("click", () => {
            btn.nextElementSibling.classList.remove("hidden");
        });
    });
    btnCloseModalAcc.forEach((btn) => {
        btn.addEventListener("click", () => {
            btn.parentNode.parentNode.parentNode.parentNode.classList.add("hidden");
        });
    });
}


if (totalTitle) {
    totalTitle.forEach((item) => {
        item.addEventListener("click", () => {
            item.nextElementSibling.classList.toggle("hidden");
            if (item.nextElementSibling.classList.contains("hidden")) {
                item.children[1].innerHTML = '<i class="fa-solid fa-chevron-down"></i>';
            } else {
                item.children[1].innerHTML = '<i class="fa-solid fa-chevron-up"></i>';
            }
        });
    });
}

if (fillter && fillterDropdown) {
    fillter.addEventListener("click", () => {
        fillterDropdown.classList.toggle("hidden");
    });
    fillterDropdownChild.forEach((item) => {
        item.addEventListener("click", () => {
            fillterDropdown.classList.toggle("hidden");
        });
    });
}
