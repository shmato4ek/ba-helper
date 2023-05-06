import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../store/reducer';
import Page from '../../components/Page/Page';
import { firebaseAuth } from '../../firebase';
import { InterceptorService } from '../../services/Interceptors';
import { config } from '../../config';
import styled from 'styled-components';
import HeaderContainer from '../HeaderContainer/HeaderContainer';
import { Navigate, Outlet, Route, Routes, useLocation, useNavigate } from 'react-router';
import { AppAction } from '../../store/actions';
import history from '../../services/history';
import { globals } from '../../services/is';
import { Link } from 'react-router-dom';

// Bright Gray (#EEEEEE)
// Gainsboro (#DDDDDD)
// Chinese Silver (#CCCCCC)
// X11 Gray (#BBBBBB)
// Dark Charcoal (#333333)

const Footer = styled.footer`
  padding: 20px;
  grid-area: footer;
  background-color: #cbe9c8;
`;

const MainGrid = styled.div`
  display: grid;
  grid-gap: 0px;
  grid-template-rows: auto 1fr auto;
  grid-template-areas: "header" "content" "footer";
`;

const AppContainer = () => {
  const location = useLocation();
  const dispatch = useDispatch();
  const state = useSelector((state: AppState) => state);

  useEffect(()=> {
    InterceptorService.init(config);

    if (state.actions.getMe.success === null) {
      dispatch<AppAction>({ type: 'GET_ME' });
    }
  }, [dispatch, state.actions.getMe.success]);

  if(state.actions.getMe.success === null) {
    return <div>Loading</div>
  }

  // if(
  //   state.me === null
  //   && location.pathname !== '/' && location.pathname !== '/login'
  //   ) {
  //   return <Navigate to='/login' />
  // }

  return (
    <Page>
      <MainGrid>
        <HeaderContainer/>
        <Outlet />
        <Footer>2023 production</Footer>
      </MainGrid>
    </Page>
  );
};

export default AppContainer;
