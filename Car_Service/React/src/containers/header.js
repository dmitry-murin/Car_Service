import { connect } from 'react-redux'

import Header from '../components/Header/index.jsx';
import {logoutUser} from '../actions';

const mapStateToProps = state => ({
    isLogin: state.app.user.token!=null,
    role: state.app.user.role
})

const mapDispatchToProps = dispatch => ({
    logoutUser:()=>{
        dispatch(logoutUser())
    }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Header)