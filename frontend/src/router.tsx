import { createBrowserRouter, createRoutesFromElements, Route } from "react-router-dom"
import NotFound from "./components/NotFound/NotFound";
import ServicesPage from "./containers/pages/ServicesPage/ServicesPage";
import AppContainer from "./containers/AppContainer/AppContainer"
import ProjectsPage from "./containers/pages/ProjectsPage/ProjectsPage";
import FrontPage from "./containers/pages/FrontPage/FrontPage";
import LoginPage from "./containers/pages/LoginPage/LoginPage";
import ProjectPage from "./containers/pages/ProjectPage/ProjectPage";
import PostProjectPage from "./containers/pages/PostProjectPage/PostProjectPage";
import ProfilePage from "./containers/pages/ProfilePage/ProfilePage";
import DocumentsPage from "./containers/pages/DocumentsPage/DocumentsPage";
import PostDocumentPage from "./containers/pages/PostDocumentPage/PostDocumentPage";
import ProjectStatsPage from "./containers/pages/ProjectStatsPage/ProjectStatsPage";
import ProfileStatisticsPage from "./containers/pages/ProfileStatisticsPage/ProfileStatisticsPage";
import ProjectStatsPageV2 from "./containers/pages/ProjectStatsPageV2/ProjectStatsPageV2";

export default createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<AppContainer />}>
      <Route index element={<FrontPage />} />
      {/* <Route path="services" element={<ServicesPage />} /> */}
      <Route path="my-projects" element={<ProjectsPage />} />
      <Route path="owned-projects" element={<ProjectsPage />} />
      <Route path="archived-projects" element={<ProjectsPage />} />
      <Route path="projects/:projectId" element={<ProjectPage />} />
      <Route path="projects/statistics/:projectId" element={<ProjectStatsPage />} />
      <Route path="projects/statistics/dbscan/:projectId" element={<ProjectStatsPageV2 />} />
      <Route path="projects/new" element={<PostProjectPage />} />
      <Route path="profile" element={<ProfilePage />} />
      <Route path="profile/statistics" element={<ProfileStatisticsPage />} />
      <Route path="documents" element={<DocumentsPage />} />
      <Route path="documents/new" element={<PostDocumentPage />} />
      <Route path="login" element={<LoginPage />} />
      <Route path="*" element={<NotFound />}/>
    </Route>
  )
);
