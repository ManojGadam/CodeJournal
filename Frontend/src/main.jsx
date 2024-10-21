import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { App } from './App';
import './index.css';

// const router = createBrowserRouter([
//   {
//     path: '/',
//     element: <App />
//   },
// ]);

// createRoot(document.getElementById('root')).render(
//   <StrictMode>
//     <RouterProvider router={router} />
//   </StrictMode>,
// );



import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";

const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(
    <React.StrictMode>
        <Router>
            <App />
        </Router>
    </React.StrictMode>
);
