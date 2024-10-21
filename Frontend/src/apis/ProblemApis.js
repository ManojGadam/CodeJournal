import axios from 'axios';

const domain = "http://localhost:5000"

export function getProblems(){
   return axios.get(domain + "/api/Problem/GetProblems", {
    headers: {
        'Content-Type': 'application/json' // Ensure content type is set
    }})
}


export function updateProblem(data){
    return axios.post(`${domain}/api/Problem/AddProblem/`,data,{
        headers: {
            'Content-Type': 'application/json' // Ensure content type is set
        }});
}

export function addProblem(url){
    return axios.post(`${domain}/api/Problem/SaveProblem`,url, {
        headers: {
            'Content-Type': 'application/json' // Ensure content type is set
        }});
}

export function SaveConfiguration(data){
    return axios.post(`${domain}/api/Problem/SaveConfiguration/`,data, {
        headers: {
            'Content-Type': 'application/json' // Ensure content type is set
        }});
}

export function pushToGit(data){
    return axios.post(`${domain}/api/Problem/PushToGit/`,data, {
        headers: {
            'Content-Type': 'application/json' // Ensure content type is set
        }});
}