import React from "react";

import Navigation from "../../components/Navigation/Navigation";
import SideDrawer from "../../components/Navigation/SideDrawer/SideDrawer";

class Layout extends React.Component {
    state = {
        showSideDrawer: false
    };

    sideDrawerClosedHandler = () => this.setState({ showSideDrawer: false });

    drawerToggleClicked = () =>
        this.setState(prevState => {
            return { showSideDrawer: !prevState.showSideDrawer };
        });

    render() {
        return (
            <React.Fragment>
                <Navigation ToggleButtonClicked={this.drawerToggleClicked} />
                <SideDrawer
                    open={this.state.showSideDrawer}
                    closed={this.sideDrawerClosedHandler}
                />
                <main>{this.props.children}</main>
            </React.Fragment>
        );
    }
}

export default Layout;
