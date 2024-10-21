import React from 'react';
import { Route, Routes } from 'react-router-dom';
import './App.css';
import Header from './components/Header';
import Cards from './components/Cards';
import Footer from './components/Footer';
import { allRoutes } from '../Routes';
import {ProblemContainer} from "./containers/ProblemContainer"
export function App() {
  return (
    <div>
      <Header />
      <ProblemContainer />
      <Footer />
    </div>
  );
}
