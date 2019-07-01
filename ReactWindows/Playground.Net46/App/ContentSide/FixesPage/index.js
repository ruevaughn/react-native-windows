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
    logger: PropTypes.func,
    isFocusable: PropTypes.bool
  }

  constructor(props) {
    super(props)
  }

  render() {
    return (
      <View isFocusable={this.props.isFocusable} accessibilityLabel={'Fixes layout'} style={styles.content}>
        <Text selectable={this.props.isFocusable}  accessibilityLabel={'Fixes title'} style={styles.title}>Fixes</Text>
        <SiblingZOrder isFocusable={this.props.isFocusable} style={styles.item} />
        <BorderTest isFocusable={this.props.isFocusable} style={styles.item} />
        <TouchMouseLeaveTest isFocusable={this.props.isFocusable} style={styles.item} logger={this.props.logger} />
      </View>
    )
  }
}
