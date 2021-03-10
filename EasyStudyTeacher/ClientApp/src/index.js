import React from "react";
import ReactDOM from "react-dom";
import { getStore, ReduxProvider } from "./store/setup";
import { HashRouter, Switch, Route } from "react-router-dom";
import PageLayout from "./components/layout/Layout";
import {
  getHomeRoute,
  getRegisterPageRoute,
  getCreateGroupRoute,
} from "./routes/routes";
import i18n from "./locale/resourses";
import { loginByJWT } from "./services/jwtService";
import "./styles.css";
import RegisterPage from "./pages/RegisterPage/RegisterPage";
import { I18nextProvider } from "react-i18next";

import MainPage from "./pages/MainPage/MainPage";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  /**
   * Renders app UI
   */
  render() {
    const store = getStore();

    return (
      <I18nextProvider i18n={i18n}>
        <ReduxProvider store={store}>
          <HashRouter>
            <Switch>
              <Route
                exact
                path={getRegisterPageRoute()}
                component={() => {
                  return (
                    <PageLayout>
                      <RegisterPage />
                    </PageLayout>
                  );
                }}
              />
              <Route
                exact
                path={getHomeRoute()}
                component={() => {
                  return (
                    <PageLayout>
                      <MainPage />
                    </PageLayout>
                  );
                }}
              />
            </Switch>
          </HashRouter>
        </ReduxProvider>
      </I18nextProvider>
    );
  }
}

ReactDOM.render(<App />, document.getElementById("root"));
