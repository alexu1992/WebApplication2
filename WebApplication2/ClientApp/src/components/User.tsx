import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as UserStore from '../store/User';
import '../styles/Timer.css'
// At runtime, Redux will merge together...
type UserProps =
    UserStore.UserState // ... state we've requested from the Redux store
    & typeof UserStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ username: string }>; // ... plus incoming routing parameters


class User extends React.PureComponent<UserProps> {
    public state = {
        countTime: 0,
        isDone: true
    };
    // This method is called when the component is first added to the document
    public componentWillMount() {
        this.startTimer();
    }


    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Welcome to my page</h1>
                <p>Have fun here</p>
                <p>Change query string for switch users </p>
                {this.renderUser()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        const username = this.props.match.params.username;
        this.props.requestUser(username);
        this.setState({
            isDone: false
        });
    }

    private renderUser() {
        return (
            <div>
                <div className="base-timer">
                    <svg className="base-timer__svg" viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
                        <g className="base-timer__circle">
                            <circle className="base-timer__path-elapsed green" cx="50" cy="50" r="45"></circle>
                        </g>
                    </svg>
                    <span id="base-timer-label" className="base-timer__label">
                        {this.state.countTime}
                    </span>
                </div>

                <div>
                    <label><b>Username</b></label>
                    <input type="text" value={this.props.user.username} readOnly />
                </div>
                <div>
                    <label><b>Password</b></label>
                    <input type="text" value={this.props.user.password} readOnly />
                </div>
            </div>

        );

    }

    private tick() {
        var date1 = this.props.user.endDate ? new Date(this.props.user.endDate) : new Date();
        var date2 = new Date();
        var dif = Math.floor((date1.getTime() - date2.getTime()) / 1000) % 60;
        if (dif > 0) {
            this.setState({
                countTime: dif,
                isDone: false
            });
        }
        else {
            this.setState({
                countTime: 0,
                isDone: true
            });
        }
    }

    private startTimer() {
        if (this.state.isDone) {
            this.ensureDataFetched();
            this.tick();
            var interval = setInterval(() => {
                this.tick();
                if (this.state.isDone) {
                    clearInterval(interval);
                    this.startTimer();
                }
            },
                1000);
        }
    }



}

export default connect(
    (state: ApplicationState) => state.user, // Selects which state properties are merged into the component's props
    UserStore.actionCreators // Selects which action creators are merged into the component's props
)(User as any);
