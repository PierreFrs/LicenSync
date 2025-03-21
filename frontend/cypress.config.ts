import { defineConfig } from 'cypress'

export default defineConfig({
  projectId: 'nm12p3',
  e2e: {
    'baseUrl': 'http://localhost:4200',
    video: true,
  },
  "defaultCommandTimeout": 10000,
  "pageLoadTimeout": 60000,
  "requestTimeout": 5000,
  "responseTimeout": 30000
});
