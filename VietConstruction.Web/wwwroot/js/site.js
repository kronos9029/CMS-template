const navToggle = document.querySelector("[data-nav-toggle]");
const navMenu = document.querySelector("[data-nav-menu]");
const siteHeader = document.querySelector("[data-site-header]");
const siteNav = document.querySelector(".site-nav");
const siteBrand = document.querySelector(".site-brand");
const navList = document.querySelector(".site-nav__list");
const navCta = document.querySelector(".button--nav");
const mobileBreakpoint = 1200;

if (navToggle && navMenu && siteNav && siteBrand && navList) {
    const updateNavigationLayout = () => {
        const shouldCollapseForViewport = window.innerWidth <= mobileBreakpoint;
        const wasOpen = navMenu.classList.contains("is-open");

        siteNav.classList.remove("is-collapsed");
        navMenu.classList.remove("is-open");

        const availableWidth = siteNav.clientWidth;
        const requiredWidth =
            siteBrand.getBoundingClientRect().width +
            navList.scrollWidth +
            (navCta?.getBoundingClientRect().width ?? 0) +
            48;
        const shouldCollapseForContent = !shouldCollapseForViewport && requiredWidth > availableWidth;
        const isCollapsed = shouldCollapseForViewport || shouldCollapseForContent;

        siteNav.classList.toggle("is-collapsed", isCollapsed);

        if (isCollapsed && wasOpen) {
            navMenu.classList.add("is-open");
        }
    };

    navToggle.addEventListener("click", () => {
        navMenu.classList.toggle("is-open");
    });

    updateNavigationLayout();
    window.addEventListener("resize", updateNavigationLayout);
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
