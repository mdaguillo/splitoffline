import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://my-json-server.typicode.com/mdaguillo/mock-server',
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
     }
});

export default {
    getExpenses() {
        return apiClient.get('/expenses');
    }
}