var selectSkillID,selectRate, valueSkill,valueRate, inputSkill,inputRate;

function change() {
    selectSkillID = document.getElementById("selectSkillID"); // Выбираем  select по id
    valueSkill = selectSkillID.options[selectSkillID.selectedIndex].value; // Значение value для выбранного option  
    inputSkill = document.getElementById("hiddenSkillID");
    inputSkill.setAttribute("value", valueSkill)
    selectRate = document.getElementById("selectRate"); // Выбираем  select по id
    valueRate = selectRate.options[selectRate.selectedIndex].value; // Значение value для выбранного option  
    inputRate = document.getElementById("hiddenRate");
    inputRate.setAttribute("value", valueRate);
}