const navToggle = document.querySelector("[data-nav-toggle]");
const navMenu = document.querySelector("[data-nav-menu]");
const siteHeader = document.querySelector("[data-site-header]");

if (navToggle && navMenu) {
    navToggle.addEventListener("click", () => {
        navMenu.classList.toggle("is-open");
    });

    window.addEventListener("resize", () => {
        if (window.innerWidth > 1319) {
            navMenu.classList.remove("is-open");
        }
    });
}

if (siteHeader) {
    const handleHeaderState = () => {
        siteHeader.classList.toggle("is-scrolled", window.scrollY > 12);
    };

    handleHeaderState();
    window.addEventListener("scroll", handleHeaderState);
}

const revealTargets = document.querySelectorAll(
    ".section-block, .page-shell, .content-section, .showcase-card, .project-card, .article-card, .job-card, .fact-card"
);

if ("IntersectionObserver" in window && revealTargets.length > 0) {
    const revealObserver = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                entry.target.classList.add("is-visible");
                revealObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.12 });

    revealTargets.forEach((target, index) => {
        target.setAttribute("data-reveal", "");
        target.style.transitionDelay = `${Math.min(index % 4, 3) * 70}ms`;
        revealObserver.observe(target);
    });
}
