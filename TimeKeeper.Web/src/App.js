import React from "react";

import Layout from "./hoc/Layout/Layout";
import AboutPage from "./components/AboutPage/AboutPage";

class App extends React.Component {
    state = {};

    render() {
        return (
            <Layout>
                <AboutPage passedId={"about"} />

                <div id="services" style={{ height: "100vh", backgroundColor: "green" }} />

                <div id="team" style={{ height: "100vh", backgroundColor: "blue" }} />
            </Layout>
        );
    }
}

export default App;
