import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  View,
} from 'react-native';
import styles from './styles'
import SiblingZOrder from './SiblingZOrder'
import BorderTest from './BorderTest'
import TouchMouseLeaveTest from './TouchMouseLeaveTest'

export default class FixesPage extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  constructor(props) {
    super(props)
  }

  render() {
    return (
      <View style={styles.content}>
        <Text style={styles.title}>Fixes</Text>
        <SiblingZOrder style={styles.item} />
        <BorderTest style={styles.item} />
        <TouchMouseLeaveTest style={styles.item} logger={this.props.logger} />
      </View>
    )
  }
}
