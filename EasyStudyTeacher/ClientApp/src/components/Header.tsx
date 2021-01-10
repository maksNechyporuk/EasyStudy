import * as React from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./Header.css";

export default class Header extends React.PureComponent<
  {},
  { isOpen: boolean }
> {
  public state = {
    isOpen: false,
  };

  public render() {
    return (
      <header>
        <div className="main" id="banner"></div>
        {/* <Navbar classNameName="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">EasyStudy</NavbarBrand>
                        <NavbarToggler onClick={this.toggle} classNameName="mr-2"/>
                        <Collapse classNameName="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
                            <ul classNameName="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} classNameName="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} classNameName="text-dark" to="/counter">Counter</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} classNameName="text-dark" to="/fetch-data">Fetch data</NavLink>
                                </NavItem>
                                <NavLink activeclassNameName="active" tag={Link} classNameName="nav-link text-dark" to="/register">Реєстрація</NavLink>
                                <NavLink activeclassNameName="active" tag={Link} classNameName="nav-link text-dark" to="/login">Логін</NavLink>

                            </ul>
                        </Collapse>
                    </Container>
                </Navbar> */}
      </header>
    );
  }

  private toggle = () => {
    this.setState({
      isOpen: !this.state.isOpen,
    });
  };
}
