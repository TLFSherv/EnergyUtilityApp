/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Pages/**/*.cshtml",           // This is the most important one
        "./Pages/**/*.razor",            // If you ever use Blazor
        "./Views/**/*.cshtml",           // In case you have MVC views
        "./Components/**/*.cshtml",      // If you use ViewComponents
        "./wwwroot/**/*.html",           // Optional
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}