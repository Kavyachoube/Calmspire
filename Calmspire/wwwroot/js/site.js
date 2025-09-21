// Main JavaScript file for CalmSpire

document.addEventListener('DOMContentLoaded', function () {
    // Initialize tooltips if Bootstrap is loaded
    if (typeof bootstrap !== 'undefined') {
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }

    // Auto-hide alerts after 5 seconds
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
    alerts.forEach(function (alert) {
        if (alert.classList.contains('alert-success') || alert.classList.contains('alert-info')) {
            setTimeout(function () {
                const bsAlert = new bootstrap.Alert(alert);
                if (bsAlert) {
                    bsAlert.close();
                }
            }, 5000);
        }
    });

    // Smooth scrolling for anchor links
    const anchorLinks = document.querySelectorAll('a[href^="#"]');
    anchorLinks.forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Add active class to current navigation item
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.nav-link');
    navLinks.forEach(function (link) {
        if (link.getAttribute('href') === currentPath) {
            link.classList.add('active');
        }
    });

    // Form validation enhancements
    const forms = document.querySelectorAll('form');
    forms.forEach(function (form) {
        form.addEventListener('submit', function (e) {
            const submitButton = form.querySelector('button[type="submit"]');
            if (submitButton && !form.classList.contains('was-validated')) {
                submitButton.disabled = true;
                submitButton.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Processing...';

                // Re-enable button after 3 seconds as fallback
                setTimeout(function () {
                    submitButton.disabled = false;
                    submitButton.innerHTML = submitButton.getAttribute('data-original-text') || 'Submit';
                }, 3000);
            }
        });

        // Store original button text
        const submitButton = form.querySelector('button[type="submit"]');
        if (submitButton) {
            submitButton.setAttribute('data-original-text', submitButton.innerHTML);
        }
    });

    // Mood slider animations
    const moodSliders = document.querySelectorAll('.mood-slider');
    moodSliders.forEach(function (slider) {
        slider.addEventListener('input', function () {
            const value = this.value;
            const percentage = ((value - this.min) / (this.max - this.min)) * 100;

            // Update slider appearance
            this.style.background = `linear-gradient(to right, #3b82f6 0%, #3b82f6 ${percentage}%, #e9ecef ${percentage}%, #e9ecef 100%)`;

            // Add haptic feedback on mobile
            if (navigator.vibrate) {
                navigator.vibrate(10);
            }
        });

        // Initialize slider appearance
        slider.dispatchEvent(new Event('input'));
    });

    // Animate numbers/counters on dashboard
    const animateNumbers = function () {
        const numbers = document.querySelectorAll('[data-animate-number]');
        numbers.forEach(function (element) {
            const target = parseInt(element.textContent);
            const duration = 1000;
            const start = performance.now();

            const animate = function (currentTime) {
                const elapsed = currentTime - start;
                const progress = Math.min(elapsed / duration, 1);

                // Easing function for smooth animation
                const easeOutQuart = 1 - Math.pow(1 - progress, 4);
                const current = Math.floor(target * easeOutQuart);

                element.textContent = current;

                if (progress < 1) {
                    requestAnimationFrame(animate);
                }
            };

            requestAnimationFrame(animate);
        });
    };

    // Trigger number animation when elements come into view
    const observeNumbers = function () {
        const observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    animateNumbers();
                    observer.disconnect();
                }
            });
        });

        const dashboard = document.querySelector('.dashboard');
        if (dashboard) {
            observer.observe(dashboard);
        }
    };

    observeNumbers();

    // Keyboard shortcuts
    document.addEventListener('keydown', function (e) {
        // Only activate shortcuts when not in form inputs
        if (e.target.tagName === 'INPUT' || e.target.tagName === 'TEXTAREA') {
            return;
        }

        // Alt + D for Dashboard
        if (e.altKey && e.key === 'd') {
            e.preventDefault();
            const dashboardLink = document.querySelector('a[href*="/Dashboard"]');
            if (dashboardLink) {
                window.location.href = dashboardLink.href;
            }
        }

        // Alt + M for Mood tracking
        if (e.altKey && e.key === 'm') {
            e.preventDefault();
            const moodLink = document.querySelector('a[href*="/Mood"]');
            if (moodLink) {
                window.location.href = moodLink.href;
            }
        }

        // Alt + J for Journal
        if (e.altKey && e.key === 'j') {
            e.preventDefault();
            const journalLink = document.querySelector('a[href*="/Journal"]');
            if (journalLink) {
                window.location.href = journalLink.href;
            }
        }
    });

    // Chat auto-scroll functionality
    const chatContainer = document.querySelector('.chat-messages');
    if (chatContainer) {
        const scrollToBottom = function () {
            chatContainer.scrollTop = chatContainer.scrollHeight;
        };

        // Auto-scroll when new messages are added
        const chatObserver = new MutationObserver(scrollToBottom);
        chatObserver.observe(chatContainer, {
            childList: true,
            subtree: true
        });
    }

    // Add loading states to buttons with data-loading attribute
    const loadingButtons = document.querySelectorAll('[data-loading]');
    loadingButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            const originalText = this.innerHTML;
            const loadingText = this.getAttribute('data-loading') || 'Loading...';

            this.innerHTML = `<i class="fas fa-spinner fa-spin me-2"></i>${loadingText}`;
            this.disabled = true;

            // Reset after 10 seconds as fallback
            setTimeout(() => {
                this.innerHTML = originalText;
                this.disabled = false;
            }, 10000);
        });
    });

    // Progressive enhancement for mood charts
    if (typeof Chart !== 'undefined') {
        // Set global Chart.js defaults
        Chart.defaults.font.family = "'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif";
        Chart.defaults.color = '#64748b';
        Chart.defaults.borderColor = '#e2e8f0';
    }
});

// Utility functions
const CalmSpire = {
    // Show notification
    showNotification: function (message, type = 'success') {
        const alertHTML = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                <i class="fas fa-${type === 'success' ? 'check-circle' : 'exclamation-circle'} me-2"></i>
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;

        const container = document.querySelector('.main-content') || document.body;
        container.insertAdjacentHTML('afterbegin', alertHTML);

        // Auto-hide after 5 seconds
        setTimeout(function () {
            const alert = container.querySelector('.alert');
            if (alert && typeof bootstrap !== 'undefined') {
                const bsAlert = new bootstrap.Alert(alert);
                bsAlert.close();
            }
        }, 5000);
    },

    // Format date for display
    formatDate: function (date, options = {}) {
        const defaultOptions = {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        };
        return new Date(date).toLocaleDateString(undefined, { ...defaultOptions, ...options });
    },

    // Animate element entrance
    animateIn: function (element, animation = 'fadeInUp') {
        element.style.opacity = '0';
        element.style.transform = 'translateY(20px)';
        element.style.transition = 'opacity 0.6s ease, transform 0.6s ease';

        requestAnimationFrame(function () {
            element.style.opacity = '1';
            element.style.transform = 'translateY(0)';
        });
    },

    // Debounce function for search/filter inputs
    debounce: function (func, wait, immediate) {
        let timeout;
        return function () {
            const context = this, args = arguments;
            const later = function () {
                timeout = null;
                if (!immediate) func.apply(context, args);
            };
            const callNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) func.apply(context, args);
        };
    }
};

// Export for use in other scripts
if (typeof module !== 'undefined' && module.exports) {
    module.exports = CalmSpire;
}
// Floating Chat Bubble Logic
