import { connect } from 'react-redux'

import Workers from '../components/Workers/index.jsx';
import {addWorker,getWorkers} from '../actions';

const mapStateToProps = state => ({
    workers: state.workers.workers,
    url: state.app.workerURL
})

const mapDispatchToProps = dispatch => ({
  addWorker:(surname,firstname, email, telephone)=>{
    dispatch(addWorker(surname,firstname, email, telephone))
    dispatch(getWorkers());
  },
  initWorkers:()=>{
    dispatch(getWorkers());
  }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Workers)