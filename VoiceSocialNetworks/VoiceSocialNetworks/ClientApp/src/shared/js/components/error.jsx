import React, { Component } from 'react'

export default class Error extends Component {
    render() {
        const { props } = this;
        return (
            <div>
                <p className="error-message">{props.errorMessage}</p>
            </div>
        )
    }
}
