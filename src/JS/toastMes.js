export function toast({ 
    message = '', 
    type = 'success', 
    duration = 3000
}) {
    const main = document.getElementById('toast');
    const icons = {
        success: 'fa-solid fa-circle-check',
        info: 'fa-solid fa-exclamation',
        warning: 'fa-solid fa-circle-check',
        error: 'fa-solid fa-circle-exclamation',
    };
    if (main){
        const toast = document.createElement('div');
        const icon = icons[type] ;
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add('toast', `toast--${type}`);
        toast.style.animation = `slideInleft ease .3s, fadeOut linear 1s ${delay}s forwards`;
        
        toast.innerHTML = 
        `
        <div class="toast__icon">
            <i class="${icon}"></i>
        </div>  
        <div class="toast__body">
            <p class="toast__msg">${message}</p>
        </div>
        <div class="toast__close">
            <i class="fa-solid fa-xmark"></i>
        </div>
        `;
        main.appendChild(toast);

        setTimeout(function() {
            main.removeChild(toast);
        }, duration + 1000 );
    }

}


