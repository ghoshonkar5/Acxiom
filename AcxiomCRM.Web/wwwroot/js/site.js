document.addEventListener('DOMContentLoaded', () => {
    // Theme toggler
    const themeToggleBtn = document.getElementById('theme-toggle');
    const htmlElement = document.documentElement;
    const themeIcon = themeToggleBtn?.querySelector('i');

    const savedTheme = localStorage.getItem('theme');
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    
    if (savedTheme === 'dark' || (!savedTheme && prefersDark)) {
        htmlElement.setAttribute('data-theme', 'dark');
        updateIcon('dark');
    }

    if (themeToggleBtn) {
        themeToggleBtn.addEventListener('click', () => {
            const currentTheme = htmlElement.getAttribute('data-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            
            htmlElement.setAttribute('data-theme', newTheme);
            localStorage.setItem('theme', newTheme);
            updateIcon(newTheme);
            window.dispatchEvent(new Event('themeChanged'));
        });
    }

    function updateIcon(theme) {
        if (!themeIcon) return;
        if (theme === 'dark') {
            themeIcon.className = 'bi bi-moon-stars-fill';
        } else {
            themeIcon.className = 'bi bi-sun-fill';
        }
    }
    
    // Sidebar toggler (Desktop & Mobile)
    const sidebarDesktopToggle = document.getElementById('sidebar-desktop-toggle');
    const sidebarMobileToggle = document.getElementById('sidebar-toggle');
    const sidebar = document.getElementById('sidebar');
    const appMain = document.querySelector('.app-main');

    // Desktop Toggle (Collapse)
    if (sidebarDesktopToggle && sidebar && appMain) {
        sidebarDesktopToggle.addEventListener('click', () => {
            sidebar.classList.toggle('collapsed');
            appMain.classList.toggle('expanded');
            
            const isCollapsed = sidebar.classList.contains('collapsed');
            localStorage.setItem('sidebarCollapsed', isCollapsed);
        });

        // Restore sidebar state
        if(localStorage.getItem('sidebarCollapsed') === 'true' && window.innerWidth > 768) {
            sidebar.classList.add('collapsed');
            appMain.classList.add('expanded');
        }
    }

    // Mobile Toggle (Show/Hide)
    if (sidebarMobileToggle && sidebar) {
        sidebarMobileToggle.addEventListener('click', () => {
            sidebar.classList.toggle('show');
            sidebar.classList.remove('collapsed'); // Mobile is always full width when shown
        });
    }
});
