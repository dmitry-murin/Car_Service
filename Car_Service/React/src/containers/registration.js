import { connect } from 'react-redux'

import Registration from '../components/Registration/index.jsx';
import {registrationUser} from '../actions';

const mapStateToProps = state => ({
})

const mapDispatchToProps = dispatch => ({
    registration:( email, pass)=>{
        dispatch(registrationUser( email, pass))
    }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Registration)