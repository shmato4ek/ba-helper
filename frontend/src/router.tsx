import { createBrowserRouter, createRoutesFromElements, Route } from "react-router-dom"
import NotFound from "./components/NotFound/NotFound";
import ServicesPage from "./containers/pages/ServicesPage/ServicesPage";
import AppContainer from "./containers/AppContainer/AppContainer"
import ProjectsPage from "./containers/pages/ProjectsPage/ProjectsPage";
import FrontPage from "./containers/pages/FrontPage/FrontPage";
import LoginPage from "./containers/pages/LoginPage/LoginPage";

export default createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<AppContainer />}>
      <Route index element={<FrontPage />} />
      <Route path="services" element={<ServicesPage />} />
      <Route path="my-projects" element={<ProjectsPage />} />
      <Route path="owned-projects" element={<ProjectsPage />} />
      <Route path="login" element={<LoginPage />} />
      <Route path="*" element={<NotFound />}/>
    </Route>
  )
);
