function show() {
    document.getElementById('testId').addEventListener('click', () => {
        const navbar = document.getElementById('collapse');
        if (Object.values(navbar.classList).includes('show')) {
            navbar.classList.remove('show');
        } else {
            navbar.classList.add('show');
        }
    });
}

show();