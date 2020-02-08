import React, { Component } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';

export default class LoginForm extends Component {

    generateStyles = () => {
        return {
            root: {
                "max-width": "480px",
                margin: "auto"
            }
        };
    }

    render() {
        //here we need to render appropriate tab value(register screen, log in screen or reset password)
        // const value = this.state.tab;
        const rootClasses = this.generateStyles();
        return (
            <AppBar position="static" classes={rootClasses}>
                <Tabs centered>
                    <Tab label="Регистрация"/>
                    <Tab label="Войти"/>
                    <Tab label="Сброс пароля"/>
                </Tabs>
            </AppBar>
        )
    }
}