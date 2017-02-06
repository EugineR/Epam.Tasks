function change() {
    selectSkillID = document.getElementById("selectSkillID"); // Выбираем  select по id
    valueSkill = selectSkillID.options[selectSkillID.selectedIndex].value; // Значение value для выбранного option  
    inputSkill = document.getElementById("hiddenSkillID");
    inputSkill.setAttribute("value", valueSkill)
    selectMinRate = document.getElementById("selectMinRate"); // Выбираем  select по id
    valueMinRate = selectMinRate.options[selectMinRate.selectedIndex].value; // Значение value для выбранного option  
    inputMinRate = document.getElementById("hiddenMinRate");
    inputMinRate.setAttribute("value", valueMinRate);
    selectMaxRate = document.getElementById("selectMaxRate"); // Выбираем  select по id
    valueMaxRate = selectMaxRate.options[selectMaxRate.selectedIndex].value; // Значение value для выбранного option  
    inputMaxRate = document.getElementById("hiddenMaxRate");
    inputMaxRate.setAttribute("value", valueMaxRate);
}