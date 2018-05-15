import { connect } from 'react-redux'

import Authentiocation from '../components/Authentication/index.jsx';
import {loginUser} from '../actions';

const mapStateToProps = state => ({
  user: state.app.user
})

const mapDispatchToProps = dispatch => ({
    login:(email, pass)=>{
        dispatch(loginUser(email, pass))
    }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Authentiocation)