/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts,scss,sass,css}",
  ],
  theme: {
    extend: {
      colors: {
        'main-bg': '#F2F2F2',
        'secondary-bg': '#e0e0e0',
        'dark-font': '#435161',
        'light-font': '#ffffff',
        'accent-bg': '#056CF2',
        'warn': '#FF0000',
      },
      fontFamily: {
        'sans': ['Roboto', 'Arial', 'sans-serif'],
        'serif': ['Arvo', 'serif'],
      }
    },
  },
  plugins: [],
}

