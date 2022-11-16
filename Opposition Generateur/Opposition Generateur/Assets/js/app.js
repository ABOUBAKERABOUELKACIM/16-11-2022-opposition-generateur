function displayToggle(id) {
     if (id.classList.contains("hidden")) {
          id.classList.remove("hidden");
     } else {
          id.classList.add("hidden");
     }
}

function accordionToggle(current, attr) {
     const allCheckboxes = document.querySelectorAll(`[role="${attr}"]`);
     for (let i = 0; i < allCheckboxes.length; i++) {
          allCheckboxes[i].checked = false;
     }
     current.checked = true;
}

function checkToggle(current, attr) {
     const allCheckboxes = document.querySelectorAll(`[role="${attr}"]`);
     for (let i = 0; i < allCheckboxes.length; i++) {
          allCheckboxes[i].checked = !allCheckboxes[i].checked;
     }
     
     if(current.checked == false) {
          current.checked = true; 
      }else if(current.checked == true) {
          current.checked = false; }

}