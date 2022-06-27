function handleApprovalStatusChange() {
    let userRoles = getUserRoles();

    let approvalStatusField = Xrm.Page.getAttribute("odh_approvalstatus");
    
    // The Resources Supervisor role is the role of the field resources supervisor and it hasn't been created yet
    if (userRoles.includes(...['System Administrator', 'Resources Supervisor']))
        approvalStatusField.setValue(753_210_001); //  Approved
    else
        approvalStatusField.setValue(753_210_000); //  Pending Approval
}



// This function retursn an array of all user Roles Like This:  
// ['CCI admin', 'System Administrator', 'IoT - Administrator']
function getUserRoles() {

    let userRoles = [];

    // The roles._collection returns an object that carry the roles objects as key value pairs value
    // {
    //      a1801436-efd6-e811-a96e-000d3a3ab886: { 
    //          id: '5e4a9faa-b260-e611-8106-00155db8820b', name: 'IoT - Administrator' 
    //          }, 
    //      7b2ae761-a60a-ec11-b6e5-000d3ab2f2cd: {…}, 
    //      5e4a9faa-b260-e611-8106-00155db8820b: {…}
    // }
    let userRolesObject = Xrm.Page.context._userSettings.roles._collection;  
    

    // This will return the key of the previous object
    Object.keys(userRolesArray).forEach(key => {
        userRoles.push(userRolesObject[key].name);
    }); 

    return userRoles;
}