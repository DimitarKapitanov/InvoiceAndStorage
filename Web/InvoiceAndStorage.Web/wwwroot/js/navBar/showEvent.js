function show() {
    document.getElementById('testId').addEventListener('click', collapse);

    function collapse() {
        event.preventDefault();
        const navbar = document.getElementById('collapse');
        console.log('in eevent', navbar)
        if(Object.values(navbar.classList).includes('show')) {
            navbar.classList.remove('show');
        } else {
            navbar.classList.add('show');
        }
    }
}