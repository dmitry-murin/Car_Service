import { connect } from 'react-redux'

import {getConfirm,setIsConfirm} from '../actions';
import Confirm from '../components/Confirm/index.jsx';

const mapStateToProps = state => ({
    isConfirm: state.app.isConfirm
})

const mapDispatchToProps = dispatch => ({
    getIsConfirm:(guid)=>{
        dispatch(getConfirm(guid))
    },
    reset:()=>{
        dispatch(setIsConfirm(undefined));
    }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Confirm)